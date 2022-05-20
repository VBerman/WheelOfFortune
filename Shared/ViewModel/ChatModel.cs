using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.ViewModel
{
    public class ChatModel
    {
        public int Id { get; set; }
        public ReadRealEstateDto RealEstate { get; set; }

        public ReadUserDto User { get; set; }
    }
}
