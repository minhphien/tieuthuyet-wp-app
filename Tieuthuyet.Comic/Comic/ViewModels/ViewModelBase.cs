using System;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace Comic.ViewModels
{
    public class ViewModelBase : Conductor<IScreen>.Collection.OneActive, INotifyPropertyChanged, IDisposable
    {
        protected INavigationService navigationService;
        protected readonly IEventAggregator eventAggregator;

        public enum ComicErrors
        {
            CANNOT_LOAD_CHAPTER,
            CANNOT_LOAD_CHAPTERCONTENT,
            CANNOT_LOAD_NOVEL,
            CANNOT_LOAD_GENRE
        }

        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                OnPropertyChanged(() => IsLoading);
            }
        }

        private bool _IsSearching;

        public bool IsSearching
        {
            get { return _IsSearching; }
            set
            {
                _IsSearching = value;
                OnPropertyChanged(() => IsSearching);
            }
        }

        private bool _IsEmpty;

        public bool IsEmpty
        {
            get { return _IsEmpty; }
            set
            {
                _IsEmpty = value;
                OnPropertyChanged(() => IsEmpty);
            }
        }

        private static int ErrorTimes = 0;

        #region Constructor

        protected ViewModelBase()
        {
            this.navigationService = IoC.Get<INavigationService>();
            this.eventAggregator = IoC.Get<IEventAggregator>();
        }

        #endregion // Constructor

        #region DisplayName

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public string DisplayName { get; set; }

        #endregion // DisplayName

        #region Debugging Aides

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            //the cast will always succeed
            MemberExpression memberExpression = (MemberExpression) exp.Body;
            string propertyName = memberExpression.Member.Name;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName,
                this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion // IDisposable Members

        public void ShowMessage(ComicErrors ComicError, Exception e)
        {
            IsEmpty = true;
            LoadingFailed();
            ErrorTimes++;
            MessageBoxResult result = MessageBox.Show(
                e.Message,
                Resources.AppResources.Error_Title,
                MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
            if (e.Message.Equals("LastestConnectionError") && ErrorTimes <= 2)
            {


            }
        }

        public virtual void LoadingFailed()
        {
            IsLoading = false;
        }

    }
}
