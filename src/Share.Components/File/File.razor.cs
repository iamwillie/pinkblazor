using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PinkBlazor
{
    /// <summary>
    /// Represents a File Input
    /// </summary>
    public partial class File : Component, IDisposable
    {
        #region Members
        ElementReference _inputFileElement;
        IDisposable _thisReference;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public File()
        {

        }
        #endregion

        #region Parameters
        /// <summary>
        /// 
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UnmatchedParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<IFileListEntry[]>OnChange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int MaxMessageSize { get; set; } = 20 * 1024; // TODO: Use SignalR default

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int MaxBufferSize { get; set; } = 1024 * 1024;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool IsDragAndDrop { get; set; }
        #endregion

        #region Injections
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Component Error Message
        /// </summary>
        [Parameter]
        public string ErrorMessage { get; set; } = string.Empty;
        #endregion

        #region Public
        [JSInvokable]
        public Task NotifyChange(FileListEntryImplementation[] files)
        {
            foreach (var file in files)
            {
                file.Owner = (File)(object)this;
            }

            return OnChange.InvokeAsync(files);
        }
        #endregion

        #region Overrides
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _thisReference = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeAsync<object>("BlazorInputFile.init", _inputFileElement, _thisReference);
            }
        }
        #endregion

        #region Internal
        internal Stream OpenFileStream(FileListEntryImplementation file)
        {
            var isSupported = SharedMemoryFileListEntryStream.IsSupported(JSRuntime) ?
                (Stream)new SharedMemoryFileListEntryStream(JSRuntime, _inputFileElement, file) :
                new RemoteFileListEntryStream(JSRuntime, _inputFileElement, file, MaxMessageSize, MaxBufferSize);

            return isSupported;
        }
        #endregion

        void IDisposable.Dispose()
        {
            _thisReference?.Dispose();
        }
    }
}
