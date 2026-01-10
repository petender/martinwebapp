# Quick Start - Deploy to Azure

## 🚀 Deploy in 3 Commands

### 1. Login to Azure
```bash
azd auth login
```

### 2. Deploy Everything
```bash
azd up
```
**What it does:**
- ✅ Creates Resource Group
- ✅ Provisions Azure SQL Database
- ✅ Creates App Service (Linux + .NET 9.0)
- ✅ Sets up Key Vault with secrets
- ✅ Configures Application Insights
- ✅ Deploys your application code
- ✅ Returns your live application URL

**Time**: ~5-10 minutes

### 3. Open Your App
The deployment will provide a URL like:
```
https://app-abc123.azurewebsites.net
```

## 🔑 Same Login Credentials

- **Username**: `machen` (or jawilson, prpatel, etc.)
- **Password**: `Doctor123!`

## 📊 View Metrics
```bash
azd monitor
```

## 🔄 Update After Code Changes
```bash
azd deploy
```

## 🗑️ Delete Everything
```bash
azd down
```

## 💰 Expected Monthly Cost
**~$18-20 USD** (Basic tier for App Service + SQL Database)

## 📚 Full Documentation
- **Azure Deployment Guide**: [AZURE_DEPLOYMENT.md](AZURE_DEPLOYMENT.md)
- **Architecture Plan**: [azd-arch-plan.md](azd-arch-plan.md)
- **Full README**: [README.md](README.md)

---

**Ready to deploy?** Just run: `azd up`
