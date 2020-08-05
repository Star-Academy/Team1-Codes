package search.test;

import java.io.File;
import java.util.*;

import org.junit.*;

import search.*;

public class InvertedIndexTest {
    InvertedIndex invertedIndex = new InvertedIndex();
    HashMap<File, List<String>> allTokens;

    @BeforeClass
    public void provideTokens() {
        allTokens = new HashMap<>();
        File file1 = new File("file1");
        File file2 = new File("file2");
        String[] file1_tokens = { "star", "mohammad", "phase", "three" };
        String[] file2_tokens = { "star", "alireza", "three", "javad" };
        allTokens.put(file1, Arrays.asList(file1_tokens));
        allTokens.put(file2, Arrays.asList(file2_tokens));
    }

    @Before
    public void setUp() {
        invertedIndex.initMap(allTokens);
    }

    @After
    public void tearDown() {
        invertedIndex = new InvertedIndex();
    }

    @Test
    public void initMapTest() {
        HashSet<String> expectedFiles = new HashSet<>();
        expectedFiles.add("file1");
        Assert.assertEquals(expectedFiles, invertedIndex.allWords.get("phase"));
        expectedFiles.add("file2");
        Assert.assertEquals(expectedFiles, invertedIndex.allWords.get("star"));
    }

    @Test
    public void orQueryResultTest() {
        String[] queries = {"phase", "3"};
        ArrayList<String> orQueries = new ArrayList<>(Arrays.asList(queries));
        
        HashSet<String> result = invertedIndex.orQueryResult(orQueries);
        Assert.assertTrue(result.contains("file1"));
        Assert.assertFalse(result.contains("file2"));
    }

    @Test
    public void andQueryResultTest() {
        String[] queries = {"phase", "three"};
        ArrayList<String> andQueries = new ArrayList<>(Arrays.asList(queries));
        
        HashSet<String> result = invertedIndex.andQueryResult(andQueries);
        Assert.assertTrue(result.contains("file1"));
        Assert.assertFalse(result.contains("file2"));
        andQueries.add("notAToken");
        result = invertedIndex.andQueryResult(andQueries);
        Assert.assertTrue(result == null);
    }

    @Test
    public void excQueryResultTest() {
        String[] queries = {"phase", "three"};
        ArrayList<String> excQueries = new ArrayList<>(Arrays.asList(queries));
        
        HashSet<String> result = invertedIndex.excQueryResult(excQueries);
        Assert.assertTrue(result.size() == 2);
    }

}