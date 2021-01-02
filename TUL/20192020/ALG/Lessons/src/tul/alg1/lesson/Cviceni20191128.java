package tul.alg1.lesson;


import java.util.Scanner;

import static tul.alg1.lesson.tools.ArrayTools.*;


public class Cviceni20191128 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        printArray(" Prvocisla ", primeNumber(100));
    }

    public static int[] primeNumber(int k) {
        boolean[] c = new boolean[k + 1];
        for (int i = 0; i < c.length; i++) {
            c[i] = true;
        }
        int count = 0;
        for (int i = 2; i < c.length; i++) {
            if (c[i]) {
                count++;
                for (int j = i; j <= (c.length - 1) / i; j++) {
                    c[j * i] = false;
                }
            }
        }
        int[] a = new int[count];
        int j = 0;
        for (int i = 2; i < c.length; i++) {
            if (c[i]) {
                a[j++] = i;
            }
        }
        return a;
    }

}
