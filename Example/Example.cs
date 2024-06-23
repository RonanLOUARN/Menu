﻿using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Menu;
using Menu.Enums;

namespace example;

public class Example : BasePlugin
{
    public override string ModuleName => "pl1";
    public override string ModuleVersion => "1.0.0";
    public Menu.Menu Menu { get; } = new();

    public override void Load(bool isReload)
    {
        AddCommand("css_test", "", (controller, _) =>
        {
            var cursor = new MenuValue[2]
            {
                new("►") { Prefix = "<font color=\"#3399FF\">", Suffix = "<font color=\"#FFFFFF\">" },
                new("◄") { Prefix = "<font color=\"#3399FF\">", Suffix = "<font color=\"#FFFFFF\">" },
            };

            var selector = new MenuValue[2]
            {
                new("[ ") { Prefix = "<font color=\"#0033FF\">", Suffix = "<font color=\"#FFFFFF\">" },
                new(" ]") { Prefix = "<font color=\"#0033FF\">", Suffix = "<font color=\"#FFFFFF\">" },
            };

            var mainMenu = new MenuBase(new MenuValue("Main Menu"))
            {
                Cursor = cursor,
                Selector = selector
            };

            var options = new List<MenuValue>
            {
                new("option1") { Prefix = "<font color=\"#9900FF\">", Suffix = "<font color=\"#FFFFFF\">" },
                new("option2"),
                new("option3"),
                new("option4") { Prefix = "<font color=\"#33AA33\">", Suffix = "<font color=\"#FFFFFF\">" },
                new("option5")
            };

            var choices = new List<MenuValue>
            {
                new("choice1") { Prefix = "<font color=\"#AA1133\">", Suffix = "<font color=\"#FFFFFF\">" },
                new("choice2"),
                new("choice3"),
                new("choice4") { Prefix = "<font color=\"#BB9933\">", Suffix = "<font color=\"#FFFFFF\">" },
                new("choice5")
            };

            var players = Utilities.GetPlayers().Select(player => (MenuValue)new PlayerValue(player.PlayerName, player.UserId)).ToList();
            players.Add(new PlayerValue("p1", 1) { Prefix = "<font color=\"#AA1133\">", Suffix = "<font color=\"#FFFFFF\">" });
            players.Add(new PlayerValue("p2", 2));

            var item = new MenuItem(MenuItemType.ChoiceBool, options);
            var itemPinwheel = new MenuItem(MenuItemType.Choice, new MenuValue("h: "), choices, new MenuValue(" :t"), true);
            var itemPlayers = new MenuItem(MenuItemType.Button, new MenuValue("button: ") { Prefix = "<font color=\"#AA33CC\">", Suffix = "<font color=\"#FFFFFF\">" }, players, new MenuValue(" :tail") { Prefix = "<font color=\"#DDAA11\">", Suffix = "<font color=\"#FFFFFF\">" });
            var itemPlayersPinwheel = new MenuItem(MenuItemType.Button, new MenuValue("button: "), players, true);

            mainMenu.AddItem(item);
            mainMenu.AddItem(itemPinwheel);
            mainMenu.AddItem(itemPlayers);
            mainMenu.AddItem(itemPlayersPinwheel);
            
            Menu.AddMenu(controller!, mainMenu, (callback =>
            {
                Console.WriteLine(callback);
            }));
        });
    }
}

public class PlayerValue(string value, int? id) : MenuValue(value)
{
    public int? Id { get; set; } = id;
    //public Player? Player { get; set; }
    //public CCSPlayerController? Controller { get; set; }
}