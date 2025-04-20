using Metsuoki.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metsuoki.Infrastructure.Persistence.Seeds;

public static class ModelBuilderExtensions
{
    public static void AddCategory(this ModelBuilder modelBuilder)
    {
        // Родительские категории
        var menCategoryId = Guid.Parse("10000000-0000-0000-0000-000000000000");

        var trousers = Guid.Parse("10000000-0001-0000-0000-000000000000");
        var outerwear = Guid.Parse("10000000-0002-0000-0000-000000000000");
        var jeans = Guid.Parse("10000000-0003-0000-0000-000000000000");
        var homeClothes = Guid.Parse("10000000-0004-0000-0000-000000000000");
        var overalls = Guid.Parse("10000000-0005-0000-0000-000000000000");
        var clothingSets = Guid.Parse("10000000-0006-0000-0000-000000000000");
        var jackets = Guid.Parse("10000000-0007-0000-0000-000000000000");
        var sportswear = Guid.Parse("10000000-0008-0000-0000-000000000000");
        var lingerie = Guid.Parse("10000000-0009-0000-0000-000000000000");

        var womenCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000000");

        modelBuilder.Entity<Category>().HasData(
        new Category
        {
            Id = menCategoryId,
            Name = "Мужчинам",
            Description = "Категории одежды для мужчин",
            ParentCategoryId = null
        },
        new Category
        {
            Id = womenCategoryId,
            Name = "Женщинам",
            Description = "Категории одежды для женщин",
            ParentCategoryId = null
        },

        #region Подкатегории для Мужчин
        #region Брюки
        new Category
        {
            Id = trousers,
            Name = "Брюки",
            ParentCategoryId = menCategoryId
        },
        #endregion

        #region Верхняя одежда
        new Category
        {
            Id = outerwear,
            Name = "Верхняя одежда",
            ParentCategoryId = menCategoryId
        },

        new Category
        {
            Id = Guid.Parse("10000000-0002-0000-0000-000000000001"),
            Name = "Дубленки мужские",
            ParentCategoryId = outerwear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0002-0000-0000-000000000002"),
            Name = "Жилеты",
            ParentCategoryId = outerwear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0002-0000-0000-000000000003"),
            Name = "Пальто",
            ParentCategoryId = outerwear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0002-0000-0000-000000000004"),
            Name = "Куртки",
            ParentCategoryId = outerwear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0002-0000-0000-000000000005"),
            Name = "Плащи",
            ParentCategoryId = outerwear
        },
        #endregion

        #region Джинсы
        new Category
        {
            Id = jeans,
            Name = "Джинсы",
            ParentCategoryId = menCategoryId
        },
        #endregion

        #region Домашняя одежда
        new Category
        {
            Id = homeClothes,
            Name = "Домашняя одежда",
            ParentCategoryId = menCategoryId
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000001"),
            Name = "Брюки и шорты домашние",
            ParentCategoryId = homeClothes
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000002"),
            Name = "Кигуруми и толстовки",
            ParentCategoryId = homeClothes
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000003"),
            Name = "Комплекты домашние",
            ParentCategoryId = homeClothes
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000004"),
            Name = "Пижамы",
            ParentCategoryId = homeClothes
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000005"),
            Name = "Футболки домашние",
            ParentCategoryId = homeClothes
        },
        new Category
        {
            Id = Guid.Parse("10000000-0004-0000-0000-000000000006"),
            Name = "Халаты",
            ParentCategoryId = homeClothes
        },
        #endregion

        #region Комбинезоны и полукомбинезоны
        new Category
        {
            Id = overalls,
            Name = "Комбинезоны и полукомбинезоны",
            ParentCategoryId = menCategoryId
        },
        #endregion

        #region Комплекты одежды
        new Category
        {
            Id = clothingSets,
            Name = "Комплекты одежды",
            ParentCategoryId = menCategoryId
        },
        #endregion

        #region Костюмы,  жилекти и пиджаки
        new Category
        {
            Id = jackets,
            Name = "Костюмы,  жилекти и пиджаки",
            ParentCategoryId = menCategoryId
        },
        #endregion

        #region Одежда для спорта
        new Category
        {
            Id = sportswear,
            Name = "Одежда для спорта",
            ParentCategoryId = menCategoryId
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000001"),
            Name = "Спортивные костюмы",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000002"),
            Name = "Спортивные брюки",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000003"),
            Name = "Спортивные куртки",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000004"),
            Name = "Спортивные шорты",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000005"),
            Name = "Спортивные майки и футболки",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000006"),
            Name = "Спортивные носки и гольфы",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000007"),
            Name = "Спортивные толстовки",
            ParentCategoryId = sportswear
        },
        new Category
        {
            Id = Guid.Parse("10000000-0008-0000-0000-000000000008"),
            Name = "Спортивные одежда для плавания",
            ParentCategoryId = sportswear
        },
        #endregion

        #region Нижнее белье
        new Category
        {
            Id = lingerie,
            Name = "Нижнее белье",
            ParentCategoryId = menCategoryId
        },
        new Category
        {
            Id = Guid.Parse("10000000-0009-0000-0000-000000000001"),
            Name = "Комплекты нижнего мужского белья",
            ParentCategoryId = lingerie
        },
        new Category
        {
            Id = Guid.Parse("10000000-0009-0000-0000-000000000002"),
            Name = "Кальсоны и колготки",
            ParentCategoryId = lingerie
        },
        new Category
        {
            Id = Guid.Parse("10000000-0009-0000-0000-000000000003"),
            Name = "Комплекты Корректирующее белье и боди",
            ParentCategoryId = lingerie
        },
        new Category
        {
            Id = Guid.Parse("10000000-0009-0000-0000-000000000004"),
            Name = "Трусы",
            ParentCategoryId = lingerie
        }
        #endregion

        #endregion

        #region Подкатегории для Женщин
        
        #endregion
    );
    }
}