using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SAASShop.Pages
{
    public abstract class TransactBasePage : SecurityPage
    {
        [BindProperty]
        public RecordingModel RecordingInfo { get; set; }

        [BindProperty]
        public CommitCancelModel CCInfo { get; set; }

        [BindProperty]
        public DeleteCancelModel DCInfo { get; set; }

        public bool AllowDelete { get; set; }

        public TransactBasePage()
        {
            RecordingInfo = new RecordingModel();
            CCInfo = new CommitCancelModel();
            DCInfo = new DeleteCancelModel();
        }

        protected void SetRecordingInfo(string recordedBy, System.DateTime recordedOn)
        {
            RecordingInfo.RecordedBy = recordedBy;
            RecordingInfo.RecordedOn = recordedOn;
        }
        protected void SetRecordingInfo(string recordedBy, DateTime recordedOn, string createdBy, DateTime? createdOn)
        {
            RecordingInfo.RecordedBy = recordedBy;
            RecordingInfo.RecordedOn = recordedOn;
            RecordingInfo.CreatedBy = createdBy;
            RecordingInfo.CreatedOn = createdOn;
        }

        protected void SetCCInfo(string cancelUrl, bool saveDisabled = false)
        {
            CCInfo.CancelUrl = cancelUrl;
            CCInfo.SaveDisabled = saveDisabled;
        }

        protected void SetDCInfo(string cancelUrl, int listId)
        {
            DCInfo.CancelUrl = cancelUrl;
            DCInfo.ListId = listId;
        }

        public bool CanDelete()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanDelete").Value);
        }

        public bool CanUnlock()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanUnlock").Value);
        }

        public bool CanApprove()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanApprove").Value);
        }

        public bool CanAcknowledge()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanAcknowledge").Value);
        }
    }

    public abstract class SecurityPage : PageModel
    {
        public bool CanDownloadCaseDocument()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanDownloadCaseDocument").Value);
        }

        public bool CanDownloadCaregiverNoteDocument()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanDownloadCaregiverNoteDocument").Value);
        }

        public bool CanDownloadReferralDocument()
        {
            return bool.Parse(User.Claims.FirstOrDefault(s => s.Type == "CanDownloadReferralDocument").Value);
        }

        // This is for Future Tricky Policies
    }
}