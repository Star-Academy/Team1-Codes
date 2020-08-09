package search.test;

import org.junit.Assert;
import org.junit.Test;
import search.Tokenizer;

import java.util.*;

public class TokenizerTest {
    @Test
    public void tokenizeLinesTest() {
        List<String> fileLines = new ArrayList<>();
        fileLines.add("Star$Academy");
        fileLines.add("phase  three");
        List<String> tokenizedLines = Tokenizer.tokenizeLines(fileLines);
        String[] tokens = {"star", "academy", "phase", "three"};
        List<String> expectedTokens = new ArrayList<>(Arrays.asList(tokens));
        Assert.assertEquals(expectedTokens, tokenizedLines);
    }
}
