using GameStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data;

// DbContext is an object that represents a session with the database that can be used to query and save your entities
// DbContextOptions to create DbContext with type of the specific Context class, options are going to provide GameStoreContext 
// the information on how to connect with database
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // GameStoreContext needs representation of objects that need to be mapped to the database

    // DbSet is an object that can be used to query and save the instances of Game
    // Any LINQ queries against DbSet of type Game will be translated into queries against the database
    // Creates DbSet instance through Set<Game>() that can be used to query and save instances of Game
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
