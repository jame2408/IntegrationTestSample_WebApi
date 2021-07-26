using System.Collections.Generic;

namespace WebApiTest.SeedData.Movie
{
    public class MoviesEntities
    {
        public static List<WebApi.Database.Models.Movie> DefaultMoviesEntity() =>
            new()
            {
                new() {Name = "復仇者聯盟：終局之戰", Rating = 6},
                new() {Name = "黑寡婦", Rating = 12},
                new() {Name = "詭屋", Rating = 18},
                new() {Name = "死亡漩渦：奪魂鋸新遊戲", Rating = 18},
                new() {Name = "玩命鈔劫", Rating = 15},
                new() {Name = "尋龍使者：拉雅", Rating = 0},
                new() {Name = "德州電鋸殺人狂", Rating = 18}
            };
    }
}