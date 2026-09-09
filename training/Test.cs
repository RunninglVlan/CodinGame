var grid = new Grid(10, 10);
grid.Get(new Coord(7, 6)).townId = 2;
var neighbors = GetAllNeighboursOfRailBlock(new Coord(7, 6));
Console.Error.WriteLine(string.Join(", ", neighbors));
return;

List<Coord> GetAllNeighboursOfRailBlock(Coord cursor) {
    var neighs = new List<Coord>();
    var fifo = new Queue<Coord>();
    new PriorityQueue<int, int>();
    fifo.Enqueue(cursor);
    var visited = new HashSet<Coord> { cursor };
    while (fifo.TryDequeue(out var current)) {
        bool usableTrack = grid.Get(current).IsTrackOrTown();
        if (!usableTrack) {
            neighs.Add(current);
            continue; // not part of rail block
        }
        foreach (var n in grid.GetNeighbours(current)) {
            if (visited.Add(n)) {
                fifo.Enqueue(n);
            }
        }
    }
    return neighs;
}

class Grid {
    static readonly Coord[] ADJACENCY = [Coord.NORTH, Coord.EAST, Coord.SOUTH, Coord.WEST];

    public Dictionary<Coord, Tile> cells;

    public Grid(int width, int height) {
        cells = [];
        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                Coord coord = new Coord(x, y);
                Tile cell = new Tile(coord);
                cells.Add(coord, cell);
            }
        }
    }

    List<Coord> GetNeighbours(Coord pos, Coord[] adjacency) {
        List<Coord> neighs = [];
        foreach (Coord delta in adjacency) {
            var n = new Coord(pos.X + delta.X, pos.Y + delta.Y);
            if (Get(n) != Tile.NO_TILE) {
                neighs.Add(n);
            }
        }
        return neighs;
    }

    public List<Coord> GetNeighbours(Coord pos) {
        return GetNeighbours(pos, ADJACENCY);
    }

    public Tile Get(Coord current) {
        return cells.GetValueOrDefault(new Coord(current.X, current.Y), Tile.NO_TILE);
    }
}

record Tile(Coord Coord, int Type = -1) {
    public int track = TRACK_NONE;
    public int townId = TOWN_NONE;
    public static readonly Tile NO_TILE = new(new Coord(-1, -1));

    public static int TRACK_NONE = -1;
    public static int TRACK_NEUTRAL = 2;
    public static int TOWN_NONE = -1;

    public bool IsTown() {
        return townId != TOWN_NONE;
    }

    public bool IsTrackOrTown() {
        return IsTown() || IsTrack();
    }

    public bool IsTrack() {
        return track != TRACK_NONE;
    }
}

record struct Coord(int X, int Y) {
    public static readonly Coord NORTH = new(0, -1);
    public static readonly Coord SOUTH = new(0, 1);
    public static readonly Coord WEST = new(-1, 0);
    public static readonly Coord EAST = new(1, 0);
}
