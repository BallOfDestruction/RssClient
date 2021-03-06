package asura.com.rssclient.data

import androidx.lifecycle.LiveData
import androidx.room.*

/**
 * The Data Access Object for the [RssItem] class
 */
@Dao
interface RssItemDao {

    @Query("SELECT * FROM rss_items")
    fun getRssItemsList() : LiveData<List<RssItem>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertRssItem(rssItem: RssItem) : Long

    @Delete
    fun deleteRssItem(rssItem: RssItem)

    @Query("SELECT * FROM rss_items WHERE rss_id = :rssId")
    fun getItemById(rssId : Long) : LiveData<RssItem>
}