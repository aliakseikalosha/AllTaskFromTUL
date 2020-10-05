/**
 * @author aliaskei.kalosha
 * Tento program musí ověřit testovací sadu náhodných čísel podle Korelačního koeficientu.
 */
package tul.alg1.zapoctovauloha;

import java.util.Random;
import java.util.Scanner;

public class Kalosha05 {
    private static Scanner sc = new Scanner(System.in);
    private static Random rnd = new Random();

    /**
     * Vstupní bod programu
     * @param args tato metoda ne použiva žadný parametr
     */
    public static void main(String[] args) {
        while (continueToWork()) {
            double min = getDouble("Dolni mez intervalu : ");
            double max = getDouble("Horni mez intervalu : ");
            int n = getInt("Zadej pocet generovanych hodnot : ");
            int s = getInt("Zadej delku kroku pro vytvoreni dvoic : ");
            double[][] m = generatePair(generateArray(min, max, n), s);

            System.out.println(String.format("Vypocteny korelacni koeficient ma hodnotu  %.3f", pearsonCorrelationCoefficient(m)));
        }
    }

    /**
     * Metoda, která počítá Korelační koeficient
     * @param m pole dvoic čísel
     * @return Korelační koeficient
     */
    private static double pearsonCorrelationCoefficient(double[][] m) {
        double s = 0;
        double xS = sample(m, true);
        double yS = sample(m, false);
        for (int i = 0; i < m.length; i++) {
            s += (m[i][0] - xS) * (m[i][1] - yS);
        }

        return s / (Math.sqrt(sqSubtraction(m, xS, true)) * (Math.sqrt(sqSubtraction(m, yS, false))));
    }

    /**
     * Metoda, která počítá rozdíl mezi X nebo Y i-tým (to záleží na proměnné useX) a průměrem, a to všechno na druhou.
     * @param m pole dvoic čísel
     * @param sample aritmeticky prúměr
     * @param useX pocitat pro první číslo
     * @return součet rozdilu prvků a průměru na druhou
     */
    private static double sqSubtraction(double[][] m, double sample, boolean useX) {
        double s = 0;
        for (int i = 0; i < m.length; i++) {
            s += (m[i][useX ? 0 : 1] - sample) * (m[i][useX ? 0 : 1] - sample);
        }
        return s;
    }

    /**
     * Metoda spočítá průměr pro X nebo Y, to zaleží na proměně useX
     * @param m pole dvoic čísel
     * @param useX pocitat pro první číslo
     * @return aritmetický prúměr
     */
    private static double sample(double[][] m, boolean useX) {
        double s = 0;
        for (int i = 0; i < m.length; i++) {
            s += m[i][useX ? 0 : 1];
        }
        return s * 1 / m.length;
    }
    /**
     * Metoda načítá hodnotu typu int ze standardního vstupu
     * @param s titylek
     * @return načtená hodnotá
     */
    private static int getInt(String s) {
        System.out.println(s);
        return sc.nextInt();
    }
    /**
     * Metoda načítá hodnotu typu double ze standardního vstupu
     * @param s titylek
     * @return načtená hodnotá
     */
    private static double getDouble(String s) {
        System.out.println(s);
        return sc.nextDouble();
    }
    /**
     * Metoda generuje sadu náhodných hodnot
     * @param a dolní mez
     * @param b horní mez
     * @param n počet hodnot
     * @return sada náhodných hodnot
     */
    private static double[] generateArray(double a, double b, int n) {
        double[] m = new double[n];
        for (int i = 0; i < n; i++) {
            m[i] = generateNumber(a, b);
        }
        return m;
    }
    /**
     * Metoda ze sady náhodných hodnot generuje dvojice čísel, které potřebujeme na sčítaní koeficientu
     * @param a sada čísel
     * @param step delka kroku
     * @return pole, které obsahuje dvoice čísel
     */
    private static double[][] generatePair(double[] a, int step) {
        double[][] m = new double[a.length - step][2];
        for (int i = 0; i < a.length - step; i++) {
            m[i][0] = a[i];
            m[i][1] = a[i + step];
        }
        return m;
    }
    /**
     * Metoda generuje náhodná čísla, která jsou z intervalu od a do b
     * @param a dolni mez intervalu
     * @param b horni mez intervalu
     * @return náhodné generováné číslo
     */
    private static double generateNumber(double a, double b) {
        return a + (b - a) * rnd.nextDouble();
    }

    /**
     * Tuto metodu používáme v cyklu while v metodě main. Tato metoda vrátí hodnotu true, když uživatel zadá  a/A,  nebo false, když uživatel zadá  n/N, v opačném případě zeptá ještě jednou.
     * @return napsal uživatel a/A(true) nebo n/N(false)
     */
    private static boolean continueToWork() {
        System.out.println("Pockracovat ve zpracovani (a/n) : ");
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
