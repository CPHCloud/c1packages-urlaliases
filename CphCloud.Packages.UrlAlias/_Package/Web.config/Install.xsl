<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()" />
		</xsl:copy>
	</xsl:template>
	<xsl:template match="/configuration/system.webServer/modules">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()" />
			<xsl:if test="count(add[@name='UrlAlias'])=0">
				<add name="UrlAlias" type="CphCloud.Packages.UrlAliasHttpModule, CphCloud.Packages.UrlAlias" />
			</xsl:if>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>