package tul.alg1.lesson;

import java.util.Scanner;

public class Lesson20191119 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        //Max();
        //System.out.println("\n-----------------\n");
        Maxv2();

        //ArrayExsample();
        //Array2DExsample();
        //ArrayOperation();
        //Sum();
        //PrintArray(FillIntArrayUnknownSize());
    }

    public static void Max() {
        int n = sc.nextInt();
        int a;
        for (int j = 0; j < n; j++) {

            int[] max = new int[4];
            int[] count = new int[4];
            for (int i = 0; i < 4; i++) {
                max[i] = -1;
                count[i] = 0;
            }

            while ((a = sc.nextInt()) > 0) {
                if (max[0] < a) {
                    max[3] = max[2];
                    max[2] = max[1];
                    max[1] = max[0];
                    max[0] = a;
                    count[3] = count[2];
                    count[2] = count[1];
                    count[1] = count[0];
                    count[0] = 0;
                } else if (max[1] < a && max[0] != a) {
                    max[3] = max[2];
                    max[2] = max[1];
                    max[1] = a;
                    count[3] = count[2];
                    count[2] = count[1];
                    count[1] = 0;
                } else if (max[2] < a && max[0] != a && max[1] != a) {
                    max[3] = max[2];
                    max[2] = a;
                    count[3] = count[2];
                    count[2] = 0;
                } else if (max[3] < a && max[0] != a && max[1] != a && max[2] != a) {
                    max[3] = a;
                    count[3] = 0;
                }
                for (int i = 0; i < 4; i++) {
                    if (max[i] == a) {
                        count[i]++;
                    }
                }
            }
            System.out.println();
            for (int i = 0; i < 4; i++) {
                System.out.println(max[i] + " " + count[i] + "\n");
            }
        }
    }
    public static void Maxv2() {
        int n = sc.nextInt();
        int a;
        for (int j = 0; j < n; j++) {

            int[] max = new int[4];
            int[] count = new int[4];
            for (int i = 0; i < 4; i++) {
                max[i] = -1;
                count[i] = 0;
            }

            while ((a = sc.nextInt()) > 0) {
                for (int i = 0; i < max.length; i++) {
                    boolean haveIt = false;
                    for (int k = 0; k < i; k++) {
                        if(max[k] == a){
                            haveIt = true;
                            break;
                        }
                    }
                    if (max[i] < a && !haveIt) {
                        for (int k = max.length - i - 1; k > 0; k--) {
                            max[k] = max[k - 1];
                            count[k] = count[k - 1];
                        }
                        max[i] = a;
                        count[i] = 1;
                        break;
                    }
                    if(max[i] == a){
                        count[i]++;
                    }
                }
            }
            for (int i = 0; i < 4; i++) {
                System.out.println(max[i] + " " + count[i] + "\n");
            }
        }
    }

    private static void ArrayExsample() {
        byte[] byteArray = new byte[1];
        byteArray[0] = 1;
        System.out.println("Data v pole byteArray " + byteArray[0]);
        float[] floats = new float[2];
        floats[0] = (float) Math.random() * 100;
        floats[1] = (float) Math.random() * 100;
        System.out.println("Rozdil 0 a 1 ve floats " + (floats[0] - floats[1]));
    }

    private static void Array2DExsample() {
        byte[][] bytes = new byte[5][4];
        bytes[0][0] = 1;
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 4; j++) {
                bytes[i][j] = (byte) (Math.random() * Byte.MAX_VALUE * 2 - Byte.MAX_VALUE);
                System.out.print(bytes[i][j] + "\t");
            }
            System.out.println();
        }
    }

    private static void ArrayOperation() {
        int[] a, b;
        b = a = new int[3];
        a = null;//array stale existuje
        b = null;//ted'už ne
        a = new int[5];
        for (int i = 0; i < a.length; i++) {
            a[i] = 10;
        }
        System.out.println("Puvodni arraz : ");
        PrintArray(a);
        ChaneArray(a);
        System.out.println("Array po zmene : ");
        PrintArray(a);

    }

    private static void ChaneArray(int[] a) {
        a[0] = -1;
    }

    private static void PrintArray(int[] a) {
        for (int i = 0; i < a.length; i++) {
            System.out.print(a[i] + "\t");
        }
        System.out.println();
    }

    private static void Sum() {
        int[] a;
        int sum = 0;
        System.out.print("Pocet hodnot : ");
        int n = sc.nextInt();
        a = new int[n];
        FillIntArray(a, sc);

        for (int i = 0; i < a.length; i++) {
            sum += a[i];
        }
        System.out.println("Vysledek se rovna : " + sum);
    }


    private static int[] FillIntArrayUnknownSize() {
        int[] a = new int[0];
        int c;
        int n = 0;
        while ((c = sc.nextInt()) > 0) {
            int[] b = new int[a.length + 1];
            for (int i = 0; i < a.length; i++) {
                b[i] = a[i];
            }
            a = b;
            a[n++] = c;
        }
        return a;
    }

    private static void FillIntArray(int[] a, Scanner s) {
        for (int i = 0; i < a.length; i++) {
            System.out.print("Zadej " + (i + 1) + " hodnotu : ");
            a[i] = s.nextInt();
        }
    }

}
