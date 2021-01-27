package tul.alg1.lesson;

import java.io.IOException;
import java.util.Scanner;

import static tul.alg1.lesson.tools.ArrayTools.*;

public class Lesson20191210 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        selectSortExsaple();
        inserSortExsample();
        bubleSortExsaple();
        binarSearchExsample();
    }

    private static void binarSearchExsample() {
        System.out.println("binarSearchExsample");
        int[] a = {1, 2, 3, 4, 13, 23, 45, 68, 78, 85, 95, 100, 116};
        int n = 68;
        int s = 0;
        int e = a.length;
        int pos = -1;
        do {
            int i = s + (e - s) / 2;
            if (a[i] < n) {
                s = i;
            } else if (a[i] > n) {
                e = i;
            } else {
                pos = i;
            }
        } while (pos == -1);
        System.out.println("Index " + n + " je " + pos);
    }

    private static void selectSortExsaple() {
        System.out.println("selectSortExsaple");
        int[] a = {3, 6, 17, 8, 4, 5, 1, 15, 2};
        printArray("Puvodni pole", a);
        for (int i = 0; i < a.length; i++) {
            int m = indexOfFirst(a, min(a, i, a.length));
            int b = a[m];
            a[m] = a[i];
            a[i] = b;
        }
        printArray("Po usporadani", a);
    }

    private static void inserSortExsample() {
        System.out.println("inserSortExsample");
        int[] a = {3, 6, 17, 8, 4, 5, 1, 15, 2};
        printArray("Puvodni pole", a);
        for (int i = 0; i < a.length - 1; i++) {
            for (int j = i + 1; j > 0; j--) {
                if (a[j] < a[j - 1]) {
                    int b = a[j];
                    a[j] = a[j - 1];
                    a[j - 1] = b;
                }
            }
        }
        printArray("Po usporadani", a);
    }

    private static void bubleSortExsaple() {
        System.out.println("bubleSortExsaple");
        int[] a = {3, 6, 17, 8, 4, 5, 1, 15, 2};
        printArray("Puvodni pole", a);

        for (int j = 0; j < a.length - 1; j++) {
            for (int i = 0; i < a.length - 1 - j; i++) {
                if (a[i + 1] < a[i]) {
                    int b = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = b;
                }
            }
        }
        printArray("Po usporadani", a);
    }


}
