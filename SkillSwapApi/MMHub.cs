using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using SkillSwapAPI.Services;

namespace SkillSwapAPI;

public class MMHub : Hub
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly MMService _mmService;
    private static readonly ConcurrentQueue<string> PlayerQueue = new();
    private static readonly HashSet<string> ActivePlayers = new();

    public MMHub(IHttpClientFactory httpClientFactory, MMService mmService)
    {
        _httpClientFactory = httpClientFactory;
        _mmService = mmService;
    }

    public async Task JoinQueue(string playerId)
    {
        if (!ActivePlayers.Add(playerId))
        {
            Console.WriteLine($"Player {playerId} is already in the queue.");
            return;
        }

        PlayerQueue.Enqueue(playerId);
        Console.WriteLine($"Player {playerId} added to the matchmaking queue.");

        await ProcessQueue();
    }

    private async Task ProcessQueue()
    {
        while (PlayerQueue.Count >= 2)
        {
            if (PlayerQueue.TryDequeue(out var player1) && PlayerQueue.TryDequeue(out var player2))
            {
                var matchResult = new MatchResult
                {
                    MatchFound = true,
                    Player1 = player1,
                    Player2 = player2
                };

                Console.WriteLine($"Match found: {player1} vs {player2}");

                await Clients.All.SendAsync("FindMatch", matchResult.Player1, matchResult.Player2);

                ActivePlayers.Remove(player1);
                ActivePlayers.Remove(player2);
            }
        }
    }
}

public class MatchResult
{
    public bool MatchFound { get; set; }
    public string? Player1 { get; set; }
    public string? Player2 { get; set; }
}