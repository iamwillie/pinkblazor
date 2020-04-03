﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PinkBlazor
{
    internal class SharedMemoryFileListEntryStream : FileListEntryStream
    {
        private readonly static Type MonoWebAssemblyJSRuntimeType
            = Type.GetType("Mono.WebAssembly.Interop.MonoWebAssemblyJSRuntime, Mono.WebAssembly.Interop");
        private static MethodInfo _cachedInvokeUnmarshalledMethodInfo;

        public SharedMemoryFileListEntryStream(IJSRuntime jsRuntime, ElementReference inputFileElement, FileListEntryImplementation file)
            : base(jsRuntime, inputFileElement, file)
        {
        }

        public static bool IsSupported(IJSRuntime jsRuntime)
        {
            return MonoWebAssemblyJSRuntimeType != null
                && MonoWebAssemblyJSRuntimeType.IsAssignableFrom(jsRuntime.GetType());
        }

        protected override async Task<int> CopyFileDataIntoBuffer(long sourceOffset, byte[] destination, int destinationOffset, int maxBytes, CancellationToken cancellationToken)
        {
            await _jsRuntime.InvokeAsync<string>(
                "BlazorInputFile.ensureArrayBufferReadyForSharedMemoryInterop",
                cancellationToken,
                _inputFileElement,
                _file.Id);

            var methodInfo = GetCachedInvokeUnmarshalledMethodInfo();
            return (int)methodInfo.Invoke(_jsRuntime, new object[]
            {
                "BlazorInputFile.readFileDataSharedMemory",
                new ReadRequest
                {
                    InputFileElementReferenceId = _inputFileElement.Id,
                    FileId = _file.Id,
                    SourceOffset = sourceOffset,
                    Destination = destination,
                    DestinationOffset = destinationOffset,
                    MaxBytes = maxBytes,
                }
            });
        }

        private static MethodInfo GetCachedInvokeUnmarshalledMethodInfo()
        {
            if (_cachedInvokeUnmarshalledMethodInfo == null)
            {
                foreach (var possibleMethodInfo in MonoWebAssemblyJSRuntimeType.GetMethods())
                {
                    if (possibleMethodInfo.Name == "InvokeUnmarshalled" && possibleMethodInfo.GetParameters().Length == 2)
                    {
                        _cachedInvokeUnmarshalledMethodInfo = possibleMethodInfo
                            .MakeGenericMethod(typeof(ReadRequest), typeof(int));
                        break;
                    }
                }

                if (_cachedInvokeUnmarshalledMethodInfo == null)
                {
                    throw new InvalidOperationException("Could not find the 2-param overload of InvokeUnmarshalled");
                }
            }

            return _cachedInvokeUnmarshalledMethodInfo;
        }

        struct ReadRequest
        {
            public string InputFileElementReferenceId;
            public int FileId;
            public long SourceOffset;
            public byte[] Destination;
            public int DestinationOffset;
            public int MaxBytes;
        }
    }
}
