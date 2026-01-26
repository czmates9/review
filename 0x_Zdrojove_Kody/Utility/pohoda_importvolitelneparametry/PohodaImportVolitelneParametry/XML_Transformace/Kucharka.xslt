<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <head>
        <title>Kuchařka</title>
      </head>
      <body>
        <xsl:for-each select="/kucharka/recept">
          <h2>
            <xsl:value-of select="./@nazev"/>
          </h2>
          <ul>
            <xsl:for-each select="./ingredience">
              <li>
                <xsl:value-of select="./@nazev"/> : <xsl:value-of select="./@mnozstvi"/> x
                <xsl:value-of select="./@jednotka"/>
              </li>
            </xsl:for-each>
          </ul>
          <p>
            <xsl:value-of select="./postup"/>
          </p>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>