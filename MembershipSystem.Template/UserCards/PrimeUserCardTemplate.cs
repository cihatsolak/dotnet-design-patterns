namespace MembershipSystem.Template.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("<a href='#' class='card-link'>Mesaj gönder</a>");
            stringBuilder.Append("<a href='#' class='card-link'>Detaylı profil</a>");
            return stringBuilder.ToString();
        }

        protected override string SetPicture()
        {
            return $"<img class='card-img-top' src='{AppUser.PictureUrl}'>";
        }
    }
}
