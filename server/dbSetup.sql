-- NOTE | To acquire keptCount use the many to many which is vaultKeeps to acquire the value and also use COUNT may need to come back to keeps table later on

-- Accounts START --

CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) UNIQUE COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

SELECT * FROM accounts;

-- Accounts END --

-- Keeps START --

CREATE TABLE keeps (
    id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    views INT UNSIGNED NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

SELECT *
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE
    keeps.`creatorId` = '66f584ad3db49ae2a611309b';

-- Keeps END --
-- Vaults --
CREATE TABLE vaults (
    id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    isPrivate BOOLEAN NOT NULL DEFAULT false,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

SELECT *
FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
WHERE
    vaults.creatorId = '66f584ad3db49ae2a611309b'
    AND isPrivate = false
    -- Vaults --
    -- VaultKeep --

CREATE TABLE vault_keeps (
    id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    keepId INT UNSIGNED NOT NULL,
    vaultId INT UNSIGNED NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

-- Not sure if this what the test wants
SELECT keepId, COUNT(*) AS kept FROM vault_keeps GROUP BY keepId;

SELECT * FROM vault_keeps;

-- VaultKeep --