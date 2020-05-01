namespace DataTransfer.ControllerDtos
{
    public class ActionDto
    {
        private bool _isSelected;
        public string ActionName { get; set; }
        public string ActionDisplayName { get; set; }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = false; } }
    }
}
