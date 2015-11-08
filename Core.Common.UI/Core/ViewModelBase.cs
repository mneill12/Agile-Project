using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using FluentValidation.Results;
using Prism.Regions;

namespace Core.Common.UI.Core
{
    // IPropChanged + Other View-to-ViewModel linking happens automatically with the ViewModelBase
    public class ViewModelBase : ObjectBase, INavigationAware
    {
        private bool _ErrorsVisible;

        private List<ObjectBase> _Models;

        public ViewModelBase()
        {
            ToggleErrorsCommand = new DelegateCommand<object>(OnToggleErrorsCommandExecute,
                OnToggleErrorsCommandCanExecute);
        }

        public object ViewLoaded
        {
            get
            {
                OnViewLoaded();
                return null;
            }
        }

        public DelegateCommand<object> ToggleErrorsCommand { get; protected set; }

        public virtual bool ValidationHeaderVisible
        {
            get { return ValidationErrors != null && ValidationErrors.Count() > 0; }
        }

        public virtual bool ErrorsVisible
        {
            get { return _ErrorsVisible; }
            set
            {
                if (_ErrorsVisible == value)
                    return;

                _ErrorsVisible = value;
                OnPropertyChanged(() => ErrorsVisible, false);
            }
        }

        public virtual string ValidationHeaderText
        {
            get
            {
                var ret = string.Empty;

                if (ValidationErrors != null)
                {
                    var verb = (ValidationErrors.Count() == 1 ? "is" : "are");
                    var suffix = (ValidationErrors.Count() == 1 ? "" : "s");

                    if (!IsValid)
                        ret = string.Format("There {0} {1} validation error{2}.", verb, ValidationErrors.Count(), suffix);
                }

                return ret;
            }
        }

        // NavigationContext can pass objects as params through XAML to different viewmodels

        // Event triggered when view is navigated to
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        // Target the same view if true, used for reinitializing views when 'false'
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        // Event triggered when view is navigated from
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        protected virtual void OnViewLoaded()
        {
        }

        protected void WithClient<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            var disposableClient = proxy as IDisposable;
            if (disposableClient != null)
                disposableClient.Dispose();
        }

        protected virtual void AddModels(List<ObjectBase> models)
        {
        }

        protected void ValidateModel()
        {
            if (_Models == null)
            {
                _Models = new List<ObjectBase>();
                AddModels(_Models);
            }

            _ValidationErrors = new List<ValidationFailure>();

            if (_Models.Count > 0)
            {
                foreach (var modelObject in _Models)
                {
                    if (modelObject != null)
                        modelObject.Validate();

                    _ValidationErrors = _ValidationErrors.Union(modelObject.ValidationErrors).ToList();
                }

                OnPropertyChanged(() => ValidationErrors, false);
                OnPropertyChanged(() => ValidationHeaderText, false);
                OnPropertyChanged(() => ValidationHeaderVisible, false);
            }
        }

        protected virtual void OnToggleErrorsCommandExecute(object arg)
        {
            ErrorsVisible = !ErrorsVisible;
        }

        protected virtual bool OnToggleErrorsCommandCanExecute(object arg)
        {
            return !IsValid;
        }
    }
}