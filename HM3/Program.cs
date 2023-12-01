using Labyrinth;

int[,] labyrinth =
{
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    {1, 0, 0, 0, 1, 1, 2, 0, 1, 1, 0, 0, 1, 2, 1, 1, 0, 0, 1, 1, 2, 0, 1 },
    {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
    {1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
    {1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1 },
    {1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 2, 0, 1, 0, 0, 0, 1, 0, 0, 1, 2, 1 },
    {1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1 },
    {1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1 },
    {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1 },
    {1, 2, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 2, 0, 1, 0, 0, 0, 1, 0, 1, 1 },
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
};

List<(int x, int y)> foundExits = [];

(int x, int y) startPosition = labyrinth.GetRandomPosition();

SearchExits(labyrinth, startPosition);

void SearchExits(int[,] labyrinth, (int x, int y) position)
{
    Thread.Sleep(10);
    if (labyrinth.IsExit(position))
    {
        foundExits.Add(position);
        labyrinth.Print(foundExits);
        return;
    }

    labyrinth.SetChecked(position);
    labyrinth.Print(foundExits);

    if (labyrinth.CheckLeft(position)) SearchExits(labyrinth, (position.x - 1, position.y));
    if (labyrinth.CheckRight(position)) SearchExits(labyrinth, (position.x + 1, position.y));
    if (labyrinth.CheckUp(position)) SearchExits(labyrinth, (position.x, position.y - 1));
    if (labyrinth.CheckDown(position)) SearchExits(labyrinth, (position.x, position.y + 1));
    return;
}

/*
██████████████████████████████████████████████
██......████►◄..████....██►◄████....████►◄..██
██..██......██......██..██......██......██..██
██..██████..██..██..██..██████..██..██..██..██
██......██..................██..............██
██..██....██████..████████....██..██..████████
██....██......██....██►◄..██......██....██►◄██
████....████..████..████..██████..████......██
██....██████....██............██..██..████..██
██..██......██......████████..██..██..██....██
██►◄██..██......██......██►◄..██......██..████
██████████████████████████████████████████████
count of exits -> 7
*/