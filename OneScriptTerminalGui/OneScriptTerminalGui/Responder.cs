namespace ostgui
{
    public class Responder : Terminal.Gui.Responder
    {
        public Terminal.Gui.Responder M_Responder;

        public new bool CanFocus
        {
            get { return M_Responder.CanFocus; }
            set { M_Responder.CanFocus = value; }
        }

        public new bool Enabled
        {
            get { return M_Responder.Enabled; }
            set { M_Responder.Enabled = value; }
        }

        public new bool Visible
        {
            get { return M_Responder.Visible; }
            set { M_Responder.Visible = value; }
        }

        public new bool HasFocus
        {
            get { return M_Responder.HasFocus; }
        }

        public new void Dispose()
        {
            M_Responder.Dispose();
        }

        public new string ToString()
        {
            return M_Responder.ToString();
        }
    }
}
