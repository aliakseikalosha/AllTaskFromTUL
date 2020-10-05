package tul.alg1.lesson.tools;

public class MathTools {

    public static double sin(double x, double eps) {
        double sin = 0;
        double mult = 1;

        int i = 0;
        while (Math.abs(mult) > eps) {
            mult = Math.pow(-1, i) * Math.pow(x, 2 * i + 1) / (double) (factorial(2 * i + 1));
            sin += mult;
            i++;
        }
        return sin;
    }

    public static long factorial(long f) {
        if (f == 0) {
            return 1;
        }
        return f * factorial(f - 1);
    }

    public static boolean equals(float a, float b, float esp) {
        return equals((double) a, (double) b, (double) esp);
    }

    public static boolean equals(double a, double b, double esp) {
        return Math.abs(a - b) < esp;
    }
}
