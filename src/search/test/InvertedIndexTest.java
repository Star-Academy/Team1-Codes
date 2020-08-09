package search.test;

import java.io.File;
import java.util.*;

import org.junit.*;

import search.*;

public class InvertedIndexTest {
    InvertedIndex invertedIndex = new InvertedIndex();
    static HashMap<File, List<String>> allTokens;

    @BeforeClass
    public static void provideTokens() {
        allTokens = new HashMap<>();
        File file1 = new File("file1");
        File file2 = new File("file2");
        String[] file1_tokens = {"star", "mohammad", "phase", "three"};
        String[] file2_tokens = {"star", "alireza", "three", "javad"};
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
        Assert.assertEquals(expectedFiles, invertedIndex.getAllWords().get("phase"));
        expectedFiles.add("file2");
        Assert.assertEquals(expectedFiles, invertedIndex.getAllWords().get("star"));
    }
}