/// <summary>
/// This queue is circular. When people are added via AddPerson, then they are added to the
/// back of the queue (per FIFO rules). When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.
/// Thus, each person stays in the queue and is given turns. When a person is added to the queue,
/// a turns parameter is provided to identify how many turns they will be given. If the turns is 0 or
/// less, they will stay in the queue forever. If a person is out of turns, then they will
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person.</param>
    /// <param name="turns">Number of turns remaining.</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them.
    /// A person with more than one turn remaining is placed back into
    /// the queue with one fewer turn.
    /// A person with zero or negative turns has infinite turns and
    /// is placed back into the queue without changing their Turns value.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        // Zero or negative turns means infinite turns.
        if (person.Turns <= 0)
        {
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            person.Turns--;
            _people.Enqueue(person);
        }

        // If Turns == 1, this was the person's final turn,
        // so the person is not added back to the queue.

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}