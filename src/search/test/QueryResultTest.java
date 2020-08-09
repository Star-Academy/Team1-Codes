package search.test;

import org.junit.*;

import search.QueryResult;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;

public class QueryResultTest {
    QueryResult queryResult;

    @Before
    public void setUpWords() {
        HashMap<String, HashSet<String>> allWords = new HashMap<>();
        HashSet<String> files = new HashSet<>();
        files.add("file1");
        allWords.put("phase", new HashSet<>(files));
        files.add("file2");
        allWords.put("star", files);
        allWords.put("three", files);

        queryResult = new QueryResult(allWords);
    }

    @Test
    public void orQueryResultTest() {
        String[] queries = {"phase", "3"};
        ArrayList<String> orQueries = new ArrayList<>(Arrays.asList(queries));

        HashSet<String> result = queryResult.orQueryResult(orQueries).getFoundDocs();
        Assert.assertTrue(result.contains("file1"));
        Assert.assertFalse(result.contains("file2"));
    }

    @Test
    public void andQueryResultTest() {
        String[] queries = {"phase", "three"};
        ArrayList<String> andQueries = new ArrayList<>(Arrays.asList(queries));

        HashSet<String> result = queryResult.andQueryResult(andQueries).getFoundDocs();
        Assert.assertTrue(result.contains("file1"));
        Assert.assertFalse(result.contains("file2"));
        andQueries.add("notAToken");
        result = queryResult.andQueryResult(andQueries).getFoundDocs();
        Assert.assertNull(result);
    }

    @Test
    public void excQueryResultTest() {
        String[] queries = {"phase", "three"};
        ArrayList<String> excQueries = new ArrayList<>(Arrays.asList(queries));

        HashSet<String> result = queryResult.excQueryResult(excQueries).getFoundDocs();
        Assert.assertEquals(2, result.size());
    }

}
