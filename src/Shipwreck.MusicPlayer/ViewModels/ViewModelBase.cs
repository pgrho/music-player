using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty(ref string field, string value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (value != field)
            {
                field = value;
                if (propertyName != null)
                {
                    RaisePropertyChanged(propertyName);
                }
                onChanged?.Invoke();
                return true;
            }
            return false;
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (!((field as IEquatable<T>)?.Equals(value) ?? Equals(field, value)))
            {
                field = value;
                if (propertyName != null)
                {
                    RaisePropertyChanged(propertyName);
                }
                onChanged?.Invoke();
                return true;
            }
            return false;
        }

        protected bool SetFlagProperty(ref byte field, byte flag, bool hasFlag, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            var nv = (byte)(hasFlag ? (field | flag) : (field & ~flag));
            return SetProperty(ref field, nv, propertyName, onChanged);
        }

        protected bool SetFlagProperty(ref ushort field, ushort flag, bool hasFlag, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            var nv = (ushort)(hasFlag ? (field | flag) : (field & ~flag));
            return SetProperty(ref field, nv, propertyName, onChanged);
        }

        protected bool SetFlagProperty(ref uint field, uint flag, bool hasFlag, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            var nv = hasFlag ? (field | flag) : (field & ~flag);
            return SetProperty(ref field, nv, propertyName, onChanged);
        }

        protected bool SetFlagProperty(ref ulong field, ulong flag, bool hasFlag, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            var nv = hasFlag ? (field | flag) : (field & ~flag);
            return SetProperty(ref field, nv, propertyName, onChanged);
        }
    }
}