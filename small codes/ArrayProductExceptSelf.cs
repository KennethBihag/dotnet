namespace ConsoleApp1
{
   internal class ArrayProductExceptSelf
   {
      static void productArray(int[] arr, int n)
      {
         if (n == 1)
         {
            Console.Write(0);
            return;
         }

         int[] left = new int[n];
         int[] right = new int[n];
         int[] prod = new int[n];

         int i, j;

         left[0] = 1;

         right[n - 1] = 1;

         for (i = 1; i < n; i++)
            left[i] = arr[i - 1] * left[i - 1];

         for (j = n - 2; j >= 0; j--)
            right[j] = arr[j + 1] * right[j + 1];

         for (i = 0; i < n; i++)
            prod[i] = left[i] * right[i];

         for (i = 0; i < n; i++)
            Console.Write(prod[i] + " ");

         return;
      }

    static void productArrayLessSpace(int[] arr, int n)
    {
 
        // Base case
        if (n == 1) {
            Console.Write(0);
            return;
        }
        int i, temp = 1;
 
        /* Allocate memory for the product
        array */
        int[] prod = new int[n];
 
        /* Initialize the product array as 1 */
        for (int j = 0; j < n; j++)
            prod[j] = 1;
 
        /* In this loop, temp variable contains
        product of elements on left side
        excluding arr[i] */
        for (i = 0; i < n; i++) {
            prod[i] = temp;
            temp *= arr[i];
        }
 
        /* Initialize temp to 1 for product on
        right side */
        temp = 1;
 
        /* In this loop, temp variable contains
        product of elements on right side
        excluding arr[i] */
        for (i = n - 1; i >= 0; i--) {
            prod[i] *= temp;
            temp *= arr[i];
        }
 
        /* print the constructed prod array */
        for (i = 0; i < n; i++)
            Console.Write(prod[i] + " ");
 
        return;
    }
      public static void Main()
      {
         int[] arr = { 10, 3, 5, 6, 2 };
         int n = arr.Length;
         Console.Write("The product array is :\n");

         productArrayLessSpace(arr, n);
      }
   }
}
