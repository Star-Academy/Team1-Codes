package search;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Tokenizer {
    public static List<String> tokenizeLines(List<String> fileLines) {
        ArrayList<String> fileTokens = new ArrayList<>();

        Pattern pattern = Pattern.compile("\\w+");
        for (String line : fileLines) {
            Matcher matcher = pattern.matcher(line);
            while (matcher.find())
                fileTokens.add(matcher.group().toLowerCase());
        }
        return fileTokens;
    }
}
