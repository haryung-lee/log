using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// MenuComponent에서 모든 메소드 구현함
/// </summary>
public abstract class MenuComponent
{
    /*
      MenuItem에서만 쓰거나 Menu에서만 쓸 수 있기에 기본적으로 에러 처리 해줌 
        -> 자기 역할에 맞지 않는 메소드는 오버라이드하지 않고 기본 구현 그대로 사용 가능
     */
    public virtual void add(MenuComponent menuComponent)
    {

    }

    public virtual void remove(MenuComponent menuCompoent)
    {

    }

    public virtual MenuComponent getChild(int i)
    {
        return null;
    }

    public abstract string getName();

    public abstract string getDescription();

    public abstract double getPrice();

    public abstract bool isVegetarian();

    public abstract void print();

}

public class MenuItem : MenuComponent
{
    string name;
    string description;
    bool vegetarian;
    double price;

    public MenuItem(string name, string description, bool vegetarian, double price)
    {
        this.name = name;
        this.description = description;
        this.vegetarian = vegetarian;
        this.price = price;
    }

    public override void print()
    {
        Console.Write($" {name} ");
        if (vegetarian)
        {
            Console.Write("(v), ");
        }
        Console.WriteLine($"{price} -- {description}");
    }

    public override string getName()
    {
        return name;
    }

    public override string getDescription()
    {
        return description;
    }

    public override double getPrice()
    {
        return price;
    }

    public override bool isVegetarian()
    {
        return vegetarian;
    }
}

public class Menu : MenuComponent
{
    List<MenuComponent> menuComponents = new List<MenuComponent>();
    string name;
    string description;

    public Menu(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public override void add(MenuComponent menuComponent)
    {
        menuComponents.Add(menuComponent);
    }

    public override void remove(MenuComponent menuComponent)
    {
        menuComponents.Remove(menuComponent);
    }

    public override MenuComponent getChild(int i)
    {
        return menuComponents[i];
    }

    public override void print()
    {
        Console.WriteLine($"\n{name}, {description}");
        Console.WriteLine("---------------");

        for(int i = 0; i < menuComponents.Count; i++)
        {
            menuComponents[i].print();
        }
    }

    public override string getName()
    {
        return name;
    }

    public override string getDescription()
    {
        return description;
    }

    public override double getPrice()
    {
        throw new NotImplementedException();
    }

    public override bool isVegetarian()
    {
        throw new NotImplementedException();
    }
}

// 이 코드를 사용하는 클라이언트
public class Waitress
{
    public MenuComponent allMenus;

    public Waitress(MenuComponent allMenus)
    {
        this.allMenus = allMenus;
    }

    // 메뉴 전체의 계층구조(모든 메뉴 및 메뉴 항목)를 출력하고 싶다면 그냥 최상위 메뉴의 print() 메소드만 호출하면 됨
    public virtual void printMenu()
    {
        allMenus.print();
    }
}

public class MenuTestDrive
{
    static void Main(string[] args)
    {
        MenuComponent pancakeHouseMenu = new Menu("팬케이크 하우스 메뉴", "아침 메뉴");
        MenuComponent dinnerMenu = new Menu("객체마을 식당 메뉴", "점심 메뉴");
        MenuComponent cafeMenu = new Menu("카페 메뉴", "저녁 메뉴");
        MenuComponent dessertMenu = new Menu("디저트 메뉴", "디저트를 즐겨보세요.");

        MenuComponent allMenus = new Menu("전체 메뉴", "전체 메뉴");

        allMenus.add(pancakeHouseMenu);
        allMenus.add(dinnerMenu);
        allMenus.add(cafeMenu);

        // 메뉴 항목 추가
        dinnerMenu.add(new MenuItem(
            "파스타",
            "마리나라 소스 스파게, 효모빵도 드림",
            true,
            3.89
            ));
        dinnerMenu.add(dessertMenu);

        dessertMenu.add(new MenuItem(
            "애플 파이",
            "바삭한 크러스트에 바닐라 아스크림이 얹혀 있는 애플 파이",
            true,
            1.59
            ));

        // 메뉴 항목 추가
        Waitress waitress = new Waitress(allMenus);
        waitress.printMenu();

    }
}
 