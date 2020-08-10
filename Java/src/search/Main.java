package search;


public class Main {
    static InvertedIndex invertedIndex;
    static FileReader fileReader;

    public static void main(String[] args) {
        final String DOCUMENTS_PATH = ".\\src\\search\\documents\\";
        invertedIndex = new InvertedIndex();
        fileReader = new FileReader(DOCUMENTS_PATH);
        fileReader.readAllFiles();
        invertedIndex.initMap(fileReader.getAllFilesTokens());
        new SearchQuery(invertedIndex).processQueries();
    }
}
