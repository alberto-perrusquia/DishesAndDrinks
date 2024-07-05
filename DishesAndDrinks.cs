
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

Console.WriteLine("Dishes & Drinks in the menu below $35.00");
float InitialCash= 35;
int NumDishes= 7; float MinDishPrice= 12; float MaxDishPrice=18;
int NumDrinks= 5; float MinDrinkPrice= 5; float MaxDrinkPrice= 9; 

Random random = new Random(1);

List<MenuItem> MenuItems = new List<MenuItem>();
MenuItems.Add(new MenuItem("Dish 0",MinDishPrice));
MenuItems.Add(new MenuItem("Drink 0",MinDrinkPrice));


MenuItems.Add(new MenuItem("Dish "+NumDishes.ToString(),MaxDishPrice));
MenuItems.Add(new MenuItem("Drink "+NumDrinks.ToString(),MaxDrinkPrice));

for (int i=1; i<NumDishes; i++)
{
    MenuItems.Add(new MenuItem("Dish "+i.ToString(),random,MinDishPrice,MaxDishPrice));
}

for ( int i=1; i < NumDrinks; i++)
{
    MenuItems.Add(new MenuItem("Drink "+i.ToString(),random, MinDrinkPrice, MaxDrinkPrice));
}

MenuItems = MenuItems.OrderBy(x => x.Name).ToList();

Console.WriteLine("Menu: ");
foreach (MenuItem Item in MenuItems) {Console.WriteLine(Item.ToString());}

MenuItems = MenuItems.OrderBy(x => x.Price).ToList();

Console.WriteLine();
Console.WriteLine("All posible possible combinations below $35.00 ");

PutOrder(InitialCash,"",MenuItems);


void PutOrder(float Cash, string PreviousOrdered,List<MenuItem> Menu)
{
    
    foreach ( MenuItem Item in Menu )
    {
         
        if (Cash>=Item.Price)
        {
            
            float RemainingCash=Cash-Item.Price; 
            string NewPreviousOrdered= PreviousOrdered+"|"+Item.ToString();
            Console.WriteLine(NewPreviousOrdered+" || Change: "+RemainingCash.ToString("0.00"));
            PutOrder( RemainingCash,NewPreviousOrdered, Menu);
            
        }
        else 
        {
            break;
        }
    }

}
struct MenuItem
{
    public readonly string Name; 
    public readonly float Price;
    public MenuItem(string _Name,float _Price)
    {
        Name=_Name; 
        Price=_Price;
    }
    public MenuItem(string _Name, Random _Random, float _MinPrice, float _MaxPrice)
    {
        Name=_Name;
        float _Price;
        
        do
        {
            int Dollars = _Random.Next((int)_MinPrice,(int)_MaxPrice);
            float Cents = _Random.Next(1,20);
            Cents= Cents*5/100;
            _Price=Dollars+Cents;
        } while (_Price < _MinPrice || _Price > _MaxPrice);
        Price=_Price;
        
    }
    public override string ToString()
    {
        return Name.ToString()+" ($ "+Price.ToString("0.00")+")";
    }
}
