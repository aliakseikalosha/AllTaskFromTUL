/**
 * @author aliaksei.kalosha
 * Tato vanoční úloha ma 4 prametry.Ktery uživatel zadavá ze standardního vstupu.
 * První výska stromecku (pocet trojuhelniku).
 * Druhí širka stromecku.
 * Tretí pole charů ze kterýh se sklada vnitrní ozdoba stromecku.a
 * Ctvrtýpole charů ze kterýh se sklada vnesní ozdoba stromecku.
 */
package tul.alg1.vanocniuloha;

import java.util.Random;
import java.util.Scanner;

public class Kalosha {
    private static Scanner sc = new Scanner(System.in);
    private static Random rnd = new Random();

    public static void main(String[] args) {
        while (continueToWork()) {
            int h = Math.max(1, getInt("Velikost stromu (kladne cele cisla od 1) : "));
            int w = Math.max(3, getInt("Sirka stromu (kaldne cele cisla od 3):"));
            char[] outer = getString("Zadej textovy ritezec ktere obsahuje ozdoby na kraji stroma : ").toCharArray();
            char[] inner = getString("Zadej textovyj ritezec ktery obsahuje ozdoby uvnitr stromu : ").toCharArray();
            DrawTopTreePart(w, inner, outer);
            for (int i = 0; i < h - 1; i++) {
                DrawTreePart(w, inner, outer);
            }
            DrawTreeBottom(w);
        }
    }

    private static void DrawTopTreePart(int w, char[] outer, char[] inner) {
        for (int i = 0; i < (w + 1) / 2; i++) {
            System.out.print(" ");
        }
        System.out.println(getRandomChar(outer));
        DrawTreePart(w, outer, inner);
    }

    private static void DrawTreePart(int w, char[] outer, char[] inner) {
        for (int i = 1; i <= w; i += 2) {
            for (int j = 0; j < (w - i) / 2; j++) {
                System.out.print(" ");
            }
            System.out.print(getRandomChar(outer));
            for (int j = 0; j < i; j++) {
                if (rnd.nextFloat() < 0.3f) {
                    System.out.print(getRandomChar(inner));
                } else {
                    System.out.print(".");
                }
            }
            System.out.print(getRandomChar(outer));
            System.out.println();
        }
    }

    private static void DrawTreeBottom(int w) {
        w += 1;
        int delta = Math.max(w / 6, 1);
        int height = Math.min(Math.max(w / 4, 1), 4);
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < w; j++) {
                if (j == w / 2 - delta || j == w / 2 + delta) {
                    System.out.print("|");
                } else {
                    System.out.print(" ");
                }
            }
            System.out.println();
        }
    }

    private static char getRandomChar(char[] c) {
        return c[rnd.nextInt(c.length)];
    }

    private static int getInt(String s) {
        System.out.println(s);
        return sc.nextInt();
    }

    private static String getString(String s) {
        System.out.println(s);
        return sc.next();
    }

    private static boolean continueToWork() {
        System.out.println("Nakreslit jeste jeden stromecek (a/n) : ");
        String str = sc.next();
        if (str.length() != 1) {
            return continueToWork();
        }
        char c = str.charAt(0);
        if (Character.toLowerCase(c) == 'a') {
            return true;
        } else if (Character.toLowerCase(c) == 'n') {
            return false;
        }
        return continueToWork();
    }

}
