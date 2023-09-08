namespace VidBox.Domain.Exceptions.Adminstrators
{
    public class AdminstratorNotFoundException : NotFoundException
    {
        public AdminstratorNotFoundException()
        {
            this.TitleMessage = "Adminstrator not found";
        }
    }
}
