package tul.alg1.lesson.tools;

public class NaturalNumbersTools {

    public static int numberOfDeviders(int number) {
        int count = 2;
        for (int i = 2; i <= number / 2; i++) {
            if (number % i == 0) {
                count++;
            }
        }
        return count;
    }
}
