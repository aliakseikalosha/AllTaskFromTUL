package tul.alg1.lesson;

import java.io.IOException;
import java.util.Random;
import java.util.Scanner;

import static tul.alg1.lesson.tools.ArrayTools.printArray;
import static tul.alg1.lesson.tools.MatrixTools.*;

public class Cviceni20191212 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        //TicTacToe t = new TicTacToe();
        //t.ticTacToeExsample();
        MemoryGame m = new MemoryGame();
        //m.example();
        System.out.println(print(m.gameField(4, 4)));
    }

    private static class MemoryGame {
        private final Random rnd = new Random();

        public void example() {
            for (int i = 0; i < 10; i++) {
                printArray(generatePermutation(8));
            }
        }

        public int[][] gameField(int sizex, int sizey) {
            int n = (sizex * sizey) / 2;
            int[][] a = new int[sizex][sizey];
            for (int i = 1; i <= n; i++) {
                for (int j = 0; j < 2; j++) {
                    int x = rnd.nextInt(sizex);
                    int y = rnd.nextInt(sizey);
                    while (a[x][y] != 0) {
                        x++;
                        if (x >= sizex) {
                            x = 0;
                            y = (y + 1) % sizey;
                        }
                    }
                    a[x][y] = i;
                }
            }
            return a;
        }

        private int[] generatePermutation(int size) {
            int[] a = new int[size];
            for (int i = 1; i <= size; i++) {
                int rIndex = rnd.nextInt(size);
                if (a[rIndex] == 0) {
                    a[rIndex] = i;
                } else {
                    while (a[rIndex] != 0) {
                        rIndex = (rIndex + 1) % size;
                    }
                    a[rIndex] = i;
                }
            }
            return a;
        }

    }

    private static class TicTacToe {
        public void ticTacToeExsample() {
            int[][] tm = new int[][]{{0, 1, 1, 0, 0}, {0, 1, 0, 1, 1}, {0, 1, 0, 1, 1}, {0, 1, 1, 0, 1}, {1, 0, 0, 0, 1},};
            System.out.println(print(tm));
            ticTacToeTest(tm, 3, 0);

            for (int i = 0; i < tm.length; i++) {
                for (int j = 0; j < tm[i].length; j++) {
                    ticTacToeTest(tm, i, j);
                }
            }
        }

        private void ticTacToeTest(int[][] a, int x, int y) {
            int line = ticTacToeLine(a, x, y);
            int columns = ticTacToeColumn(a, x, y);
            int dUL = tictacToeDiagUL(a, x, y);
            int dUR = tictacToeDiagUR(a, x, y);
            System.out.println(String.format(" [%d , %d] = %d  v radku %d, v sloupce %d, na diagUL %d, na diagUR %d", x, y, a[x][y], line, columns, dUL, dUR));
        }

        private int tictacToeDiagUR(int[][] a, int x, int y) {
            int c = a[x][y];
            int count = 1;
            int i = x + 1;
            int j = y + 1;
            while (i < a.length && j < a[x].length && a[i][j] == c) {
                count++;
                i++;
                j++;
            }
            i = x - 1;
            j = y - 1;
            while (i > -1 && j > -1 && a[i][j] == c) {
                count++;
                i--;
                j--;
            }
            return count;
        }

        private int tictacToeDiagUL(int[][] a, int x, int y) {
            int c = a[x][y];
            int count = 1;
            int i = x - 1;
            int j = y + 1;
            while (i > -1 && j < a[x].length && a[i][j] == c) {
                count++;
                i--;
                j++;
            }
            i = x + 1;
            j = y - 1;
            while (i < a.length && j > -1 && a[i][j] == c) {
                count++;
                i++;
                j--;
            }
            return count;
        }

        private int ticTacToeLine(int[][] a, int x, int y) {
            int c = a[x][y];
            int count = 0;

            for (int i = 0; i < a[x].length; i++) {
                int n = a[x][i];
                if (n == c) {
                    count++;
                } else if (n != c && i < y) {
                    count = 0;
                } else if (n != c && i > y) {
                    return count;
                }
            }
            return count;
        }

        private int ticTacToeColumn(int[][] a, int x, int y) {
            int c = a[x][y];
            int count = 0;

            for (int i = 0; i < a.length; i++) {
                int n = a[i][y];
                if (n == c) {
                    count++;
                } else if (n != c && i < x) {
                    count = 0;
                } else if (n != c && i > x) {
                    return count;
                }
            }
            return count;
        }

    }

}
