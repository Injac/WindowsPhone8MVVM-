using System;
namespace MVVMWindowsPhone.Core.Portable.Model
{
    public interface IUser
    {
        string Image { get; set; }
        string Url { get; set; }
        string UserName { get; set; }
    }
}
