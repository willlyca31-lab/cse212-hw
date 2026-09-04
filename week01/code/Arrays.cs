
public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Plan: Multiples Of
        // Step 1: Create a new double array with a size equal to length.
        // Step 2: Use a for loop to visit every index in the new array.
        // Step 3: For each index, multiply the input number by the index plus 1.
        // Step 4: Store the calculated multiple at the current index.
        // Step 5: After the loop finishes, return the completed array.

        // 2. Code: Multiples Of
        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // 3. Plan: Rotate Right
        // Step 1: Calculate the index where the right portion of the list begins.
        // Step 2: Use GetRange to copy the last 'amount' items into a new list.
        // Step 3: Use GetRange to copy the remaining items from the beginning
        //         of the original list into another new list.
        // Step 4: Clear the original data list so it can be rebuilt in the new order.
        // Step 5: Add the right portion to the original list using AddRange.
        // Step 6: Add the left portion to the original list using AddRange.
        // Step 7: The original list now contains the values rotated to the right.

        // 4. Code: Rotate Right
        int splitIndex = data.Count - amount;

        List<int> rightPart = data.GetRange(splitIndex, amount);
        List<int> leftPart = data.GetRange(0, splitIndex);

        data.Clear();

        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}
