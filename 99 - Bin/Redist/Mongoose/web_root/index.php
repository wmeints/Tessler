<html>
  <p>Take a look at <a href="page.lp">page.lp</a> example, which is
  an alternative to PHP+Mysql. Mongoose natively supports Lua server
  pages and has Sqlite3 functionality built in.</p>

  <form method="post">
    <input name="x" type="text" />
    <input type="submit" />
  </form>

  <? echo $_POST["x"]; ?>

  <? phpinfo(); ?>
</html>
