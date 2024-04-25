using System;
using Fortnite_API;
using Fortnite_API.Objects;
using Fortnite_API.Objects.V2;
using FortniteShopSort;

FortniteApiClient client = new();
BinaryHeapMin<FortniteItem> shop = new();

ApiResponse<BrShopV2> apiRes = await client.V2.Shop.GetBrAsync();
if (!apiRes.IsSuccess )
{
    if (apiRes.HasError)
    {
        Console.WriteLine(apiRes.Error);
    }
    else
    {
        Console.WriteLine("Api response was not successful");
    }
    
    return;
}
BrShopV2 brShopRes = apiRes.Data;


if(brShopRes.HasFeatured)
{
    ShopItemParser(brShopRes.Featured);
}


if (brShopRes.HasDaily)
{
    ShopItemParser(brShopRes.Daily);
}

foreach (FortniteItem listing in shop)
{
    Console.WriteLine(listing);
}

void ShopItemParser(BrShopV2StoreFront storeFront)
{
    foreach (BrShopV2StoreFrontEntry entry in storeFront.Entries)
    {
        foreach (BrCosmeticV2 item in entry.Items)
        {
            ItemType itemType = item.Type.Value switch
            {
                "outfit" => ItemType.Charecter,
                "backpack" => ItemType.Backpack,
                "pickaxe" => ItemType.HarvestingTool,
                "glider" => ItemType.Glider,
                "contrail" => ItemType.Contrail,
                "emote" => ItemType.Emote,
                "wrap" => ItemType.Wrap,
                "music" => ItemType.Music,
                _ => throw new Exception()
            };
            FortniteItem curr = new(itemType, entry.FinalPrice, entry.Bundle?.Name, item.Name);
            shop.Push(curr);
        }
    }
}