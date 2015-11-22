namespace Cush.MVVM
{
    public interface IView : IProductOf<View>
    {
        object DataContext { get; set; }
        void Show();
    }
}