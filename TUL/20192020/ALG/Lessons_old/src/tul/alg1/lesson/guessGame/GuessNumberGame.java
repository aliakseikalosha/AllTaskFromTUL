package tul.alg1.lesson.guessGame;

import java.util.Scanner;

public class GuessNumberGame {

    private static Scanner sc = new Scanner(System.in);
    private static int MaxNumber = 10;
    private static int MinNumber = 1;

    public static void main(String[] args) {
        System.out.println("Hra hadani cisla");
        boolean run = true;
        do {
            printMenu();
            int i = getInputMenu();
            switch (i) {
                case 0:
                    run = false;
                    break;
                case 1:
                    startMakeUpNumber();
                    break;
                case 2:
                    startGuessNumber();
                    break;
                case 3:
                    settings();
                    break;
                default:
                    System.out.println("Chybne zadana volba");
            }
        } while (run);
    }

    private static void settings() {
        System.out.println("");
        System.out.println("Nastaveni");
        System.out.println("Aktualni horni mez je " + MaxNumber);
        System.out.print("Zadej horni mez : ");
        MaxNumber = getInput();
    }

    private static void startMakeUpNumber() {
        System.out.println("");
        System.out.format("Myslim si cislo od %d do %d\n", MinNumber, MaxNumber);
        int number = (int) (Math.random() * MaxNumber) + MinNumber;
        int guess;
        int help = -1;
        do {
            guess = getInput();
            if (guess > number) {
                System.out.println("Uber");
            } else if (guess < number) {
                System.out.println("Pridej");
            }
            help++;
        } while (guess != number);
        System.out.println("Je to ono\n potreboval " + help + " tipu");
    }

    private static void startGuessNumber() {
        System.out.println("");
        System.out.format("Mysli cislo od %d do %d\n", MinNumber, MaxNumber);
        int min = MinNumber;
        int max = MaxNumber;
        int count = 0;
        boolean guessedNumber = false;
        do {
            count++;
            int guess = min + (max - min) / 2;
            System.out.println(guess + "\nJe to ono '0' je mensi '-1' je vysi '1'");
            switch (getInput()) {
                case -1:
                    max = guess;
                    break;
                case 1:
                    min = guess;
                    break;
                case 0:
                    guessedNumber = true;
                    break;
                default:
                    System.out.println("Spatna volba");
            }
        } while (!guessedNumber);
        System.out.println("Hura!\nUhadl to z " + count + " pokusu");

    }

    private static int getInputMenu() {
        System.out.print("Zadej volbu : ");
        return getInput();
    }

    private static int getInput() {
        int input = sc.nextInt();
        return input;
    }

    private static void printMenu() {
        System.out.println("1. Hrat hru jako hrac ktery hada");
        System.out.println("2. Hrat hru jako hrac ktery musli");
        System.out.println("3. Nastavit rassach");
        System.out.println("0. Konec");

    }
}
