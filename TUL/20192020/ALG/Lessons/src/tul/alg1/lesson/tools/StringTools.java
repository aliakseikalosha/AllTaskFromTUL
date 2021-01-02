package tul.alg1.lesson.tools;

public class StringTools {
    public static boolean isPalindrom(String s) {
        for (int i = 0; i < s.length() / 2; i++) {
            if (s.charAt(i) != s.charAt(s.length() - 1 - i)) {
                return false;
            }
        }
        return true;
    }

    public static boolean isPalindromIgnorCase(String s) {
        return isPalindrom(s.toLowerCase());
    }

    public static boolean isEquals(String s1, String s2) {
        if (s1.length() != s2.length()) {
            return false;
        }
        for (int i = 0; i < s1.length(); i++) {
            char c1 = s1.charAt(i);
            char c2 = s2.charAt(i);
            if (c1 != c2) {
                return false;
            }
        }
        return true;
    }

    public static boolean isEqualsIgnoreCase(String s1, String s2) {
        return isEquals(s1.toLowerCase(), s2.toLowerCase());
    }

    public static int compare(String s1, String s2) {
        if (s1.length() != s2.length()) {
            return s1.length() - s2.length();
        }
        for (int i = 0; i < s1.length(); i++) {
            char c1 = s1.charAt(i);
            char c2 = s2.charAt(i);
            if (c1 != c2) {
                return c1 - c2;
            }
        }
        return 0;
    }

    public static int compareIgnoreCase(String s1, String s2) {
        return compare(s1.toLowerCase(), s2.toLowerCase());
    }
}
