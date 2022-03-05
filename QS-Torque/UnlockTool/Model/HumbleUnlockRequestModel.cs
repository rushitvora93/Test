using FrameworksAndDrivers.Gui.Wpf;
using UnlockTool.Entities;

namespace UnlockTool.Model
{
    public class HumbleUnlockRequestModel : BindableBase
    {
        private UnlockRequest request_;

        public UnlockRequest getEntity()
        {
            return request_;
        }

        public HumbleUnlockRequestModel(UnlockRequest request)
        {
            request_ = request;
        }

        public ulong? UnlockRequestVersion
        {
            get { return request_.UnlockRequestVersion;}
            private set { RaisePropertyChanged(); }
        }
        public string QSTVersion
        {
            get { return request_.QSTVersion;}
            private set { RaisePropertyChanged(); }
        }
        public string Name
        {
            get { return request_.Name;}
            private set { RaisePropertyChanged(); }
        }
        public string Phonenumber
        {
            get { return request_.Phonenumber; }
            private set { RaisePropertyChanged(); }
        }
        public string Company
        {
            get { return request_.Company;}
            private set { RaisePropertyChanged(); }
        }
        public string Address
        {
            get { return request_.Address;}
            private set { RaisePropertyChanged(); }
        }
        public string Email
        {
            get { return request_.Email; }
            private set { RaisePropertyChanged(); }
        }
        public string Windows
        {
            get { return request_.Windows;}
            private set { RaisePropertyChanged(); }
        }
        public string PCName
        {
            get { return request_.PCName;}
            private set { RaisePropertyChanged(); }
        }
        public string FQDN
        {
            get { return request_.FQDN;}
            private set { RaisePropertyChanged(); }
        }
        public string LogedinUserName
        {
            get { return request_.LogedinUserName;}
            private set { RaisePropertyChanged(); }
        }
        public string CurrentLocalDateTime
        {
            get { return request_.CurrentLocalDateTime;}
            private set { RaisePropertyChanged(); }
        }

        public string CurrentUtcDateTime
        {
            get { return request_.CurrentUtcDateTime; }
            private set { RaisePropertyChanged(); }
        }
        public bool HashOk
        {
            get { return request_.HashOk;}
            private set { RaisePropertyChanged(); }
        }
    }
}
