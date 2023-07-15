Public Class Beverages
    Public Espresso As Integer
    Public EspressoDoppo As Integer
    Public Americano As Integer
    Public Cappliccno As Integer
    Public CaféMochas As Integer
    Public CaféLatte As Integer
    Public IcedAmericano As Integer
    Public IcedCaféMocha As Integer
    Public IcedLatte As Integer
    Public TiramisuIcedLatte As Integer
    Public IrishCoffee As Integer
    Public ClassicMojito As Integer

    Public CostHBeverages As Integer
    Public CostCBeverages As Integer
    Public HCtot As Integer



    Public pEspresso As Integer = 350
    Public pEspressoDoppo As Integer = 400
    Public pAmericano As Integer = 450
    Public pCappliccno As Integer = 480
    Public pCaféMochas As Integer = 550
    Public pCaféLatte As Integer = 520
    Public pIcedAmericano As Integer = 500
    Public pIcedCaféMocha As Integer = 550
    Public pIcedLatte As Integer = 520
    Public pTiramisuIcedLatte As Integer = 550
    Public pIrishCoffee As Integer = 720
    Public pClassicMojito As Integer = 520

    Public Function GetCostHotBeverages()
        CostHBeverages = Espresso + EspressoDoppo + Americano + Cappliccno + CaféMochas + CaféLatte
        Return CostHBeverages
    End Function
    Public Function GetCostColdBeverages()
        CostCBeverages = IcedAmericano + IcedCaféMocha + IcedLatte + TiramisuIcedLatte + IrishCoffee + ClassicMojito
        Return CostCBeverages
    End Function
    Public Function GetHCtot()
        HCtot = CostCBeverages + CostHBeverages
        Return HCtot
    End Function


End Class
