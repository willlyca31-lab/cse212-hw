using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities: "A" (1), "B" (3), "C" (2).
    // Dequeue three times.
    // Expected Result: B, C, A (highest priority first, regardless of insertion order)
    // Defect(s) Found: The loop in Dequeue used "index < _queue.Count - 1", which skips
    // the last item in the list entirely, so it can never be selected as the highest
    // priority item. Additionally, Dequeue never removed the selected item from the
    // underlying list, so the same item would be returned forever instead of the queue
    // shrinking.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue "A" (5), "B" (5), "C" (1) — A and B share the highest priority.
    // Dequeue three times.
    // Expected Result: A, B, C (ties are broken by FIFO order: the item closest to the
    // front of the queue among equal-priority items is removed first)
    // Defect(s) Found: The comparison in Dequeue used ">=" instead of ">" when looking
    // for the highest priority index. This meant that later items with an equal priority
    // would overwrite the selection of an earlier item with the same priority, so on a tie
    // the *last* matching item was returned instead of the *first*, breaking the required
    // FIFO tie-breaking rule.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it, then dequeue again on the now-empty queue.
    // Expected Result: The first Dequeue returns the item's value. The second Dequeue throws
    // an InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: Because Dequeue never removed the item from the internal list, the
    // queue never actually became empty after a single item was dequeued, so this exception
    // was never reachable once an item had been added.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Call Dequeue on a brand new, empty queue.
    // Expected Result: An InvalidOperationException is thrown with message "The queue is empty."
    // Defect(s) Found: No defect found. This case was already handled correctly by the
    // existing "_queue.Count == 0" check at the top of Dequeue.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue items with priorities that are NOT already in descending order in the
    // underlying list: "A" (1), "B" (2), "C" (1), "D" (2). Dequeue four times.
    // Expected Result: B, D, A, C (both priority-2 items come out before both priority-1 items,
    // and within each priority group, the earlier-enqueued item comes out first)
    // Defect(s) Found: Same root causes as above (skipped last index, ">=" tie-break, and
    // missing removal) combine to produce an incorrect order here as well, confirming the
    // fix handles interleaved priorities correctly, not just the simple cases above.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 1);
        priorityQueue.Enqueue("D", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }
}