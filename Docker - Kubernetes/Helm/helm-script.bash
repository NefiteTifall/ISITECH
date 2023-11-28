helm upgrade `
--install `
-n db-nicolas `
--create-namespace `
db `
--set auth.rootPassword="root" `
--set primary.persistence.size="10Gi"
