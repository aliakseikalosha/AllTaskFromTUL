package tul.alg1.lesson.tools;

public class ArrayTools {

    public static long sum(int[] a) {
        return sum(a, 0, a.length);
    }

    public static long sum(int[] a, int n) {
        return sum(a, 0, n);
    }

    public static long sum(int[] a, int s, int e) {
        long sum = 0;
        for (int i = s; i < e; i++) {
            sum += a[i];
        }
        return sum;
    }

    public static double sum(float[] a) {
        return sum(a, 0, a.length);
    }

    public static double sum(float[] a, int n) {
        return sum(a, 0, n);
    }

    public static double sum(float[] a, int s, int e) {
        double sum = 0;
        for (int i = s; i < e; i++) {
            sum += a[i];
        }
        return sum;
    }

    public static long mult(int[] a) {
        return mult(a, a.length);
    }

    public static long mult(int[] a, int n) {
        long m = 1;
        for (int i = 0; i < n; i++) {
            m *= a[i];
        }
        return m;
    }

    public static double mult(float[] a) {
        return mult(a, a.length);
    }

    public static double mult(float[] a, int n) {
        long m = 1;
        for (int i = 0; i < n; i++) {
            m *= a[i];
        }
        return m;
    }

    /**
     * Vrarti maximalni hodnotu v pole
     *
     * @param a pole hodnot
     * @return maximalni hodnota
     */
    public static int max(int[] a) {
        return max(a, 0, a.length);
    }


    public static int max(int[] a, int startIndex, int endIndex) {
        int max = -Integer.MAX_VALUE;
        for (int i = startIndex; i < endIndex; i++) {
            if (max < a[i]) {
                max = a[i];
            }
        }
        return max;
    }

    /**
     * Vrati minimalni hodnotu v pole
     *
     * @param a pole hodnot
     * @return hodnota minimuma
     */
    public static int min(int[] a) {
        return min(a, 0, a.length);
    }


    public static int min(int[] a, int startIndex, int endIndex) {
        int min = Integer.MAX_VALUE;
        for (int i = startIndex; i < endIndex; i++) {
            if (min > a[i]) {
                min = a[i];
            }
        }
        return min;
    }

    /**
     * Vratin pozice prvniho vyskitu cisla v pole hodnot
     *
     * @param a pole hodnot
     * @param n cislo ktere hledame
     * @return pozice cisla n v pole hodnot a
     */
    public static int indexOfFirst(int[] a, int n) {
        return indexOfFirst(a, a.length, n);
    }

    /**
     * Vrati pozicie prvniho vyskitu cisla n v prvnich l prcich  pole hodnot a
     *
     * @param a pole hodnot
     * @param l maximalni pizice dokud budeme hledata cislo n
     * @param n cislo ktere hledame
     * @return pozice cisla n
     */
    public static int indexOfFirst(int[] a, int l, int n) {
        for (int i = 0; i < l; i++) {
            if (a[i] == n) {
                return i;
            }
        }
        return -1;
    }

    /**
     * Vrati posledni pozice vyskitu cisla n v pole a
     *
     * @param a pole hodnot
     * @param n cislo ktere hledame
     * @return pocledni pozice vyskity cisla v pole a
     */
    public static int indexOfLast(int[] a, int n) {
        return indexOfLast(a, a.length, n);
    }

    /**
     * Vrati posledni pozice vyskitu cisla n v pole a
     *
     * @param a pole hodnot
     * @param l maximani pozice dokud hledame cislo n
     * @param n cislo ktere hledame
     * @return pocledni pozice vyskity cisla v pole a
     */
    public static int indexOfLast(int[] a, int l, int n) {
        for (int i = l - 1; i > -1; i++) {
            if (a[i] == n) {
                return i;
            }
        }
        return -1;
    }

    /**
     * Vyhledava pozice prvniho vyskitu maximuma v pole
     *
     * @param a pole
     * @return pozice maximuma
     */
    public static int indexOfMax(int[] a) {
        return indexOfFirst(a, max(a));
    }

    /**
     * @param a
     * @return
     */
    public static int indexOfMin(int[] a) {
        return indexOfFirst(a, min(a));
    }

    public static boolean isSortedAscendingOrder(double[] a) {
        for (int i = 0; i < a.length - 1; i++) {
            if (a[i] > a[i + 1]) {
                return false;
            }
        }
        return true;
    }

    public static boolean isArithmeticProgression(double[] a, int n) {
        if (a.length < 3) {
            double eps = 1E-5;
            double d = a[1] - a[0];
            for (int i = 2; i < n; i++) {
                if (Math.abs((a[i - 1] - a[i]) - d) < eps) {
                    return false;
                }
            }
        } else {
            return false;
        }
        return true;
    }

    public static void reverseArray(int[] a) {
        int b;
        for (int i = 0; i < a.length / 2; i++) {
            b = a[a.length - 1 - i];
            a[a.length - 1 - i] = a[i];
            a[i] = b;
        }
    }

    public static <T> boolean isNullorEmpty(T[] a) {
        return a == null || a.length < 1;
    }

    private static <T> void checkForNull(T[] a) {
        if (a == null) {
            throw new NullPointerException("Je prazne pole");
        }
    }

    public static void printArray(String title, float[] a, int n) {
        System.out.println(title);
        for (int i = 0; i < n; i++) {
            System.out.print(a[i] + "\t");
        }
        System.out.println();
    }

    public static void printArray(String title, float[] a) {
        System.out.println(title);
        //for (int i = 0; i < a.length; i++) {
        //    System.out.print(a[i] + "\t");
        //}
        for (float i : a) {
            System.out.format("%f\t", i);
        }
        System.out.println();
    }

    public static void printArray(String title, String[] a) {
        for (String s : a) {
            System.out.print("|" + s);
        }
        System.out.println("|");
    }

    public static void printArray(String title, int[] a) {
        System.out.println(title);
        printArray(a);
    }

    public static void printArray(int[] a) {
        //for (int i = 0; i < a.length; i++) {
        //    System.out.print(a[i] + "\t");
        //}
        for (int i : a) {
            System.out.format("%d\t", i);
        }
        System.out.println();
    }

}
