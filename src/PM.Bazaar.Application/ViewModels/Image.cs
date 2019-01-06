using System;
using PM.Bazaar.Application.ViewModels.Common;

namespace PM.Bazaar.Application.ViewModels
{
    public class ImageViewModel : ViewModel
    {
        public byte[] Bytes { get; set; }
    }

    public class RegisterImageViewModel : ViewModel
    {
        public byte[] Bytes { get; set; }
        public Guid Hash { get; set; }
    }
}
