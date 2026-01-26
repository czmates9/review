<?xml version="1.0"  encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                version="1.0"
                xmlns:saxon="http://icl.com/saxon"
                extension-element-prefixes="saxon">

  <xsl:output method="html" encoding="windows-1250"
              saxon:character-representation="native" indent="yes"/>

  <!-- for easy selection of Polozka elements: -->
  <xsl:key name="reports-by-lokace" match="/ReportInventura1/Polozka" use="Lokace" />

  <xsl:template match="/">





    <html>
      <head>
        <title>Tiskova sestava</title>
      </head>
      <body>


        
        <h3>
          Položky skladové inventury
        </h3>
          Inventura číslo: <xsl:value-of select="ReportInventura1/Polozka/ID_Inv"/> <span style="margin-left:75px"> Datum:
                                                    <script type="text/javascript">
                                                      d = new Date()
                                                      nmonth = d.getMonth()
                                                      ndate  = d.getDate()
                                                      nyear = d.getFullYear()
                                                      nhour  = d.getHours()
                                                      nmin   = d.getMinutes()
                                                      if(nmin &lt; 10)
                                                      {nmin = "0" + nmin}

                                                      document.write(ndate + "." + nmonth + "." + nyear + " " + nhour + ":"  +nmin)
                                                    </script>
           </span> 
        <span style="margin-left:75px"> Celkem položek:  <xsl:value-of select="count(/ReportInventura1/Polozka)"/></span>
        



        <xsl:for-each select="/ReportInventura1/Polozka[not(Lokace = preceding-sibling::Polozka/Lokace)]">
          <p></p>
          <b>
            Lokace:  <xsl:value-of select="Lokace"/> <span style="margin-left:75px"> <xsl:value-of select="Lokace"/>
            </span> <br> </br>
          </b>
          
          <table summary="Lokace: {Lokace}" rules="GROUPS"  frame="BOX">
              <THEAD>
                <tr>
                  <th>Kód položky</th>
                  <th>Čárový kód</th>
                  <th>Množství evidované</th>
                  <th>Množství skutečné</th>
                  <th></th>
                  <th></th>
                  <th></th>
                  <th width="250">Název</th>
                </tr>
              </THEAD>
              
            <xsl:for-each select="key('reports-by-lokace',Lokace)">
              <tr>
                <td>
                  <xsl:value-of select="ITEMCODE"/>
                </td>
                <td>
                  <xsl:value-of select="EAN"/>
                </td>
                <td align="right">
                  <xsl:value-of select="MnozstviEvidovane"/>
                </td>
                <td align="right">
                  <xsl:value-of select="MnozstviSkutecne"/>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                  <xsl:value-of select="Nazev"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </xsl:for-each>
        
        <p>
          <i>
            Poznámka: -
          </i>
        </p>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
