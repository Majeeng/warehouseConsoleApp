Почему-то не могу синхринизировать проект юнит теста с основным и загрузить его на гит так что напишу здсь 1 тест для примера:
Пример юнит теста для box:
public class BoxTests

{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        int id = 1;
        double height = 10;
        double width = 20;
        double length = 30;
        double weight = 5;
        DateOnly expirationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(100));
        DateOnly productionDate = DateOnly.FromDateTime(DateTime.Today);

        var box = new Box(id, height, width, length, weight, expirationDate, productionDate);

        Assert.Equal(id, box.ID);
        Assert.Equal(height, box.height);
        Assert.Equal(width, box.width);
        Assert.Equal(length, box.length);
        Assert.Equal(weight, box.weight);
        Assert.NotNull(box.expirationDate);
    }

    [Fact]
    public void Constructor_ShouldThrowIfNoDates()
    {
        Assert.Throws<Exception>(() => new Box(1, 10, 10, 10, 5, null, null));
    }
}
