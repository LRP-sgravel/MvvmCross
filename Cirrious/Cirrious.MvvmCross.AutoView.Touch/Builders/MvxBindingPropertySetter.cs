// MvxBindingPropertySetter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Interfaces.IoC;
using Cirrious.CrossCore.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.AutoView.Touch.Interfaces;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Interfaces;
using CrossUI.Core.Builder;

namespace Cirrious.MvvmCross.AutoView.Touch.Builders
{
    public class MvxBindingPropertySetter : IPropertySetter
                                            , IMvxConsumer
    {
        private readonly IMvxBindingViewController _bindingActivity;
        private readonly object _source;

        public MvxBindingPropertySetter(IMvxBindingViewController bindingActivity, object source)
        {
            _bindingActivity = bindingActivity;
            _source = source;
        }

        public void Set(object element, string targetPropertyName, string configuration)
        {
            try
            {
                var binding = this.Resolve<IMvxBinder>()
                                  .BindSingle(_source, element, targetPropertyName, configuration);
                _bindingActivity.RegisterBinding(binding);
            }
            catch (Exception exception)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "Exception thrown during the view binding {0}",
                                      exception.ToLongString());
                throw;
            }
        }
    }
}