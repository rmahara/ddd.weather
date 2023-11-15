﻿using Prism.Mvvm;
using System;

namespace DDD.WPF.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        public virtual DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}
